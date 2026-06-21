param location string = resourceGroup().location
param staticWebAppName string
param sku string = 'Free'
param skuTier string = 'Free'

resource staticWebApp 'Microsoft.Web/staticSites@2022-09-01' = {
  name: staticWebAppName
  location: location // SWA locations are limited (e.g. centralus, eastus2, westeurope)
  sku: {
    name: sku
    tier: skuTier
  }
  properties: {
    allowConfigFileUpdates: true
    provider: 'GitHub'
    enterpriseGradeCdnStatus: 'Disabled'
  }
}

output id string = staticWebApp.id
output name string = staticWebApp.name
output defaultHostname string = staticWebApp.properties.defaultHostname
