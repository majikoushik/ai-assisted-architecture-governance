param location string = resourceGroup().location
param keyVaultName string
param tenantId string = subscription().tenantId
param managedIdentityPrincipalId string = ''

resource keyVault 'Microsoft.KeyVault/vaults@2022-07-01' = {
  name: keyVaultName
  location: location
  properties: {
    sku: {
      family: 'A'
      name: 'standard'
    }
    tenantId: tenantId
    enableRbacAuthorization: true // Best practice: use RBAC instead of access policies
    enabledForDeployment: false
    enabledForTemplateDeployment: false
    enabledForDiskEncryption: false
  }
}

// If a Managed Identity is provided, grant it Key Vault Secrets User role
var secretsUserRoleDefinitionId = subscriptionResourceId('Microsoft.Authorization/roleDefinitions', '4633458b-17de-408a-b874-0445c86b69e6')

resource roleAssignment 'Microsoft.Authorization/roleAssignments@2022-04-01' = if (!empty(managedIdentityPrincipalId)) {
  name: guid(keyVault.id, managedIdentityPrincipalId, secretsUserRoleDefinitionId)
  scope: keyVault
  properties: {
    roleDefinitionId: secretsUserRoleDefinitionId
    principalId: managedIdentityPrincipalId
    principalType: 'ServicePrincipal'
  }
}

output id string = keyVault.id
output name string = keyVault.name
output uri string = keyVault.properties.vaultUri
