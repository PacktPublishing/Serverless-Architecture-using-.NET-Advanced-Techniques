New-AzureRmResourceGroup -Name myResourceGroup -Location WestUS

New-AzureRmApiManagement -ResourceGroupName "myResourceGroup" -Location "West US" -Name "apim-name" -Organization "myOrganization" -AdminEmail "myEmail" -Sku "Developer"


Remove-AzureRmResourceGroup -Name myResourceGroup