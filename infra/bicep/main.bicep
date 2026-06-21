targetScope = 'resourceGroup'

param location string = resourceGroup().location
param environmentName string = 'dev'
param appName string = 'archgov'

// SQL Configuration
param sqlAdminUsername string = 'sqladmin'
@secure()
param sqlAdminPassword string

// Container Registry Configuration
param acrSku string = 'Basic'

// Container App Configuration
param apiContainerImage string = 'mcr.microsoft.com/azuredocs/containerapps-helloworld:latest'
@secure()
param containerRegistryPassword string = ''

// Azure OpenAI Configuration
param deployAzureOpenAi bool = false
@secure()
param azureOpenAiApiKey string = ''
param azureOpenAiEndpoint string = ''
param azureOpenAiDeploymentName string = ''
param aiProvider string = 'Mock'

var resourceSuffix = '${appName}-${environmentName}-${uniqueString(resourceGroup().id)}'

module logAnalytics 'modules/log-analytics.bicep' = {
  name: 'logAnalyticsDeployment'
  params: {
    location: location
    workspaceName: 'law-${resourceSuffix}'
  }
}

module applicationInsights 'modules/application-insights.bicep' = {
  name: 'appInsightsDeployment'
  params: {
    location: location
    appInsightsName: 'appi-${resourceSuffix}'
    logAnalyticsWorkspaceId: logAnalytics.outputs.id
  }
}

module keyVault 'modules/key-vault.bicep' = {
  name: 'keyVaultDeployment'
  params: {
    location: location
    keyVaultName: 'kv-${take(resourceSuffix, 18)}' // 24 char limit
  }
}

module sqlDatabase 'modules/sql-database.bicep' = {
  name: 'sqlDatabaseDeployment'
  params: {
    location: location
    sqlServerName: 'sql-${resourceSuffix}'
    sqlDatabaseName: 'sqldb-${appName}'
    adminUsername: sqlAdminUsername
    adminPassword: sqlAdminPassword
  }
}

module containerRegistry 'modules/container-registry.bicep' = {
  name: 'containerRegistryDeployment'
  params: {
    location: location
    acrName: replace('acr${resourceSuffix}', '-', '') // Alphanumeric only
    sku: acrSku
  }
}

module containerAppsEnvironment 'modules/container-apps-environment.bicep' = {
  name: 'containerAppsEnvDeployment'
  params: {
    location: location
    environmentName: 'cae-${resourceSuffix}'
    logAnalyticsWorkspaceId: logAnalytics.outputs.id
  }
}

module containerAppApi 'modules/container-app.bicep' = {
  name: 'containerAppApiDeployment'
  params: {
    location: location
    containerAppName: 'ca-api-${resourceSuffix}'
    containerAppsEnvironmentId: containerAppsEnvironment.outputs.id
    containerImage: apiContainerImage
    containerRegistryLoginServer: containerRegistry.outputs.loginServer
    containerRegistryUsername: containerRegistry.outputs.name
    containerRegistryPassword: containerRegistryPassword
    applicationInsightsConnectionString: applicationInsights.outputs.connectionString
    sqlConnectionString: 'Server=tcp:${sqlDatabase.outputs.fullyQualifiedDomainName},1433;Initial Catalog=${sqlDatabase.outputs.databaseName};Persist Security Info=False;User ID=${sqlAdminUsername};Password=${sqlAdminPassword};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;'
    aiProvider: aiProvider
    azureOpenAiEndpoint: deployAzureOpenAi ? azureOpenAi.outputs.endpoint : azureOpenAiEndpoint
    azureOpenAiApiKey: azureOpenAiApiKey
    azureOpenAiDeploymentName: azureOpenAiDeploymentName
  }
}

module azureOpenAi 'modules/azure-openai.bicep' = if (deployAzureOpenAi) {
  name: 'azureOpenAiDeployment'
  params: {
    location: location
    accountName: 'oai-${resourceSuffix}'
  }
}

module staticWebApp 'modules/static-web-app.bicep' = {
  name: 'staticWebAppDeployment'
  params: {
    location: 'eastus2' // SWA requires specific regions
    staticWebAppName: 'swa-${resourceSuffix}'
  }
}

output apiEndpoint string = containerAppApi.outputs.fqdn
output webEndpoint string = staticWebApp.outputs.defaultHostname
