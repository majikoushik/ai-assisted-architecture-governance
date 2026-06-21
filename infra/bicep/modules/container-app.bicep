param location string = resourceGroup().location
param containerAppName string
param containerAppsEnvironmentId string
param containerImage string = 'mcr.microsoft.com/azuredocs/containerapps-helloworld:latest'
param containerRegistryLoginServer string = ''
param containerRegistryUsername string = ''
@secure()
param containerRegistryPassword string = ''

param applicationInsightsConnectionString string = ''
param sqlConnectionStringSecretName string = 'sql-connection-string'
param sqlConnectionString string = ''
param aiProvider string = 'Mock'
param azureOpenAiEndpoint string = ''
@secure()
param azureOpenAiApiKey string = ''
param azureOpenAiDeploymentName string = ''

resource containerApp 'Microsoft.App/containerApps@2023-05-01' = {
  name: containerAppName
  location: location
  properties: {
    managedEnvironmentId: containerAppsEnvironmentId
    configuration: {
      ingress: {
        external: true
        targetPort: 8080
      }
      registries: empty(containerRegistryLoginServer) ? [] : [
        {
          server: containerRegistryLoginServer
          username: containerRegistryUsername
          passwordSecretRef: 'registry-password'
        }
      ]
      secrets: [
        {
          name: 'registry-password'
          value: empty(containerRegistryPassword) ? 'placeholder' : containerRegistryPassword
        }
        {
          name: sqlConnectionStringSecretName
          value: empty(sqlConnectionString) ? 'placeholder' : sqlConnectionString
        }
        {
          name: 'azure-openai-key'
          value: empty(azureOpenAiApiKey) ? 'placeholder' : azureOpenAiApiKey
        }
      ]
    }
    template: {
      containers: [
        {
          name: containerAppName
          image: containerImage
          env: [
            {
              name: 'ASPNETCORE_ENVIRONMENT'
              value: 'Production'
            }
            {
              name: 'APPLICATIONINSIGHTS_CONNECTION_STRING'
              value: applicationInsightsConnectionString
            }
            {
              name: 'ConnectionStrings__ArchitectureGovernance'
              secretRef: sqlConnectionStringSecretName
            }
            {
              name: 'AiProvider__Provider'
              value: aiProvider
            }
            {
              name: 'AiProvider__AzureOpenAi__Endpoint'
              value: azureOpenAiEndpoint
            }
            {
              name: 'AiProvider__AzureOpenAi__ApiKey'
              secretRef: 'azure-openai-key'
            }
            {
              name: 'AiProvider__AzureOpenAi__DeploymentName'
              value: azureOpenAiDeploymentName
            }
          ]
          resources: {
            cpu: json('0.5')
            memory: '1.0Gi'
          }
          probes: [
            {
              type: 'liveness'
              httpGet: {
                path: '/health/live'
                port: 8080
              }
              initialDelaySeconds: 15
              periodSeconds: 30
            }
            {
              type: 'readiness'
              httpGet: {
                path: '/health/ready'
                port: 8080
              }
              initialDelaySeconds: 15
              periodSeconds: 30
            }
          ]
        }
      ]
      scale: {
        minReplicas: 1
        maxReplicas: 5
      }
    }
  }
}

output id string = containerApp.id
output fqdn string = containerApp.properties.configuration.ingress.fqdn
