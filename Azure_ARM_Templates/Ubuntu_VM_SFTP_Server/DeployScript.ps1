Login-AzAccount

$timeDeploy = Get-Date -format "yyyyMMddTHHmmssffff"
$deploymentName ="deploy"+ $timeDeploy
$resourceGroupName ="Nintex"
$templateUri="https://<>.blob.core.windows.net/armtemplate/azuredeploy.json"
$templateParameterUri="https://<>.blob.core.windows.net/armtemplate/azuredeploy.parameters.json"

New-AzResourceGroupDeployment -ResourceGroupName $resourceGroupName -TemplateParameterUri $templateParameterUri -TemplateUri $templateUri
