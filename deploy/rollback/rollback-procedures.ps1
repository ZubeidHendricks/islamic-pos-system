# Rollback Procedures PowerShell Script

param (
    [Parameter(Mandatory=$true)]
    [string]$ResourceGroup,
    
    [Parameter(Mandatory=$true)]
    [string]$WebAppName,
    
    [Parameter(Mandatory=$true)]
    [string]$DeploymentId
)

# Function to perform rollback
function Perform-Rollback {
    param (
        [string]$rg,
        [string]$app,
        [string]$deployId
    )
    
    try {
        # Stop the current deployment if any
        Write-Host "Stopping current deployment..."
        Stop-AzWebAppDeployment -ResourceGroupName $rg -Name $app
        
        # Revert to previous deployment
        Write-Host "Rolling back to previous deployment..."
        Set-AzWebAppSlot -ResourceGroupName $rg -Name $app -Slot production -TargetSlot staging
        
        # Verify rollback
        Write-Host "Verifying rollback..."
        $healthCheck = Invoke-WebRequest -Uri "https://$app.azurewebsites.net/health" -UseBasicParsing
        
        if ($healthCheck.StatusCode -eq 200) {
            Write-Host "Rollback successful!"
        } else {
            throw "Health check failed after rollback"
        }
    }
    catch {
        Write-Error "Error during rollback: $_"
        throw
    }
}

# Execute rollback
Perform-Rollback -rg $ResourceGroup -app $WebAppName -deployId $DeploymentId