Login-AzAccount

$timeDeploy = Get-Date -format "yyyyMMddTHHmmssffff"
$deploymentName ="deploy"+ $timeDeploy
$resourceGroupName ="Nintex"
$templateUri="https://<storageaccountname>.blob.core.windows.net/<containername>/azuredeploy.json"
$templateParameterUri="https://<storageaccountname>.blob.core.windows.net/<containername>/azuredeploy.parameters.json"

New-AzResourceGroupDeployment -ResourceGroupName $resourceGroupName -TemplateParameterUri $templateParameterUri -TemplateUri $templateUri
