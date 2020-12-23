<#*********************
    Login 
*********************#>
az login
az acr login --name **acr name** 
az acr repository list --name **acr name** --output table
docker images -a
#docker rmi 3a48e3ebf9ac -f
<#*********************
Upload Product Image
*********************#>
#Tag images
docker tag k8_product:rc1  **acr name**.azurecr.io/k8_product:1.00
#******Test Tag Image******
#docker run -d -p 8082:80  --name productservice **acr name**.azurecr.io/k8_product:1.0
#Push to ACR
docker push **acr name**.azurecr.io/k8_product:1.00

<#*********************
Upload Staff Image
*********************#>
#Tag images
docker tag k8_staff:rc1  **acr name**.azurecr.io/k8_staff:1.00
#******Test Tag Image******
#docker run -d -p 8083:80  --name staffservice **acr name**.azurecr.io/k8_staff:1.0
#Push to ACR
docker push **acr name**.azurecr.io/k8_staff:1.00

<#*********************
Upload Sales Image
*********************#>
#Tag images
docker tag k8_sales:rc1  **acr name**.azurecr.io/k8_sales:1.00
#******Test Tag Image******
#docker run -d -p 8085:80  --name saleservice **acr name**.azurecr.io/k8_sales:1.0
#Push to ACR
docker push **acr name**.azurecr.io/k8_sales:1.00

<#*********************
Upload Web Image
*********************#>
#Tag images
docker tag k8_web:rc1  **acr name**.azurecr.io/k8_web:1.00
<#******Test Tag Image******#>
#docker run -d -p 8080:80  --name webservice **acr name**.azurecr.io/k8_web:1.0
#Push to ACR
docker push **acr name**.azurecr.io/k8_web:1.00

