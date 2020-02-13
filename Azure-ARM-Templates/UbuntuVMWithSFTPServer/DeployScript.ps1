Login-AzAccount

$timeDeploy = Get-Date -format "yyyyMMddTHHmmssffff"
$deploymentName ="deploy"+ $timeDeploy
$resourceGroupName ="Nintex"
$templateUri="https://nintexdiag748.blob.core.windows.net/armtemplate/azuredeploy.json"
$templateParameterUri="https://nintexdiag748.blob.core.windows.net/armtemplate/azuredeploy.parameters.json"

New-AzResourceGroupDeployment -ResourceGroupName $resourceGroupName -TemplateParameterUri $templateParameterUri -TemplateUri $templateUri