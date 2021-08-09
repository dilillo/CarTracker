Param ([string]$subscriptionName = "Midmark Personal Subscription")

Connect-AzAccount

$tenantId = (Get-AzContext).Tenant.Id

Write-Host "Setting AzContext with tenantId = $tenantId and subscriptionName = $subscriptionName"

Set-AzContext -Subscription $subscriptionName -Tenant $tenantId

Function New-RandomString { Param ([Int]$Length = 10) return $( -join ((97..122) + (48..57) | Get-Random -Count $Length | ForEach-Object { [char]$_ })) }

$uniqueId = New-RandomString -Length 7 # Random alphanumeric string for unique resource names

$resourceGroupName = "rg-$uniqueId"

Write-Host "Creating resourceGroup $resourceGroupName"

$resourceGroup = New-AzResourceGroup -Name $resourceGroupName -Location "East Us"

Write-Host "Created resourceGroup $resourceGroup.ResourceGroupName"

$apiKind = "Sql"
$accountName = "cosmos-$uniqueId" # Must be all lower case
$consistencyLevel = "Session"
$databaseName = "carTracker"
$containerRUs = 400

Write-Host "Creating account $accountName"

$account = New-AzCosmosDBAccount -ResourceGroupName $resourceGroupName -Location "East Us" -Name $accountName -ApiKind $apiKind -DefaultConsistencyLevel $consistencyLevel

Write-Host "Creating database $databaseName"

$database = New-AzCosmosDBSqlDatabase -ParentObject $account -Name $databaseName

$containerName = "events"
$partitionKeyPath = "/aggregateID"

Write-Host "Creating container $containerName"

$container = New-AzCosmosDBSqlContainer -ParentObject $database -Name $containerName -Throughput $containerRUs -PartitionKeyKind Hash -PartitionKeyPath $partitionKeyPath
    
Write-Host "Created container $containerName"

$containerName = "views"
$partitionKeyPath = "/artificialPartitionKey"

Write-Host "Creating container $containerName"

$container = New-AzCosmosDBSqlContainer -ParentObject $database -Name $containerName -Throughput $containerRUs -PartitionKeyKind Hash -PartitionKeyPath $partitionKeyPath
    
Write-Host "Created container $containerName"

$containerName = "leases"
$partitionKeyPath = "/id"

Write-Host "Creating container $containerName"

$container = New-AzCosmosDBSqlContainer -ParentObject $database -Name $containerName -Throughput $containerRUs -PartitionKeyKind Hash -PartitionKeyPath $partitionKeyPath
    
Write-Host "Created container $containerName"