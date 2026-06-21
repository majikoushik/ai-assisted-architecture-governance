param location string = resourceGroup().location
param acrName string
param sku string = 'Basic'

resource acr 'Microsoft.ContainerRegistry/registries@2023-01-01-preview' = {
  name: acrName
  location: location
  sku: {
    name: sku
  }
  properties: {
    adminUserEnabled: false // Best practice: Use Managed Identity or Entra ID for ACR pull
  }
}

output id string = acr.id
output name string = acr.name
output loginServer string = acr.properties.loginServer
