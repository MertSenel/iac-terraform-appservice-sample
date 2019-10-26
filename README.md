# IaC-Terraform-Appservice-Sample

[![Build Status](https://dev.azure.com/mertsenel/Infrastructure-as-Code-Samples/_apis/build/status/MertSenel.iac-terraform-appservice-sample?branchName=master)](https://dev.azure.com/mertsenel/Infrastructure-as-Code-Samples/_build/latest?definitionId=2&branchName=master)

## CI/CD Sample using DotNET Core and Terraform with Azure AppServices

### Azure DevOps Project: 
https://dev.azure.com/mertsenel/Infrastructure-as-Code-Samples

### Credits:

The original source code repository used for this project can be found at: https://github.com/Microsoft/PartsUnlimited

The Initial CI/CD Structure was adopted from below Azure DevOps Lab: https://github.com/Microsoft/azuredevopslabs/tree/master/labs/vstsextend/terraform/
## Builds
https://dev.azure.com/mertsenel/Infrastructure-as-Code-Samples/_build
## Releases
https://dev.azure.com/mertsenel/Infrastructure-as-Code-Samples/_release

# Improvements/Additions:

## Project:
- Added Application Insights resource to the Terraform code to enable Contious Monitoring for the project. 

## Azure DevOps Pipelines:
### Continous Integration
- Converted GUI based Build Pipeline to YAML based build pipeline so it can be maintained with the project repository.
- Build triggers configured to target push and Pull Request CI builds in order to use as status checks for safe contribution, adhering to Git flow. 
### Continous Deployment
- Enabled Continous Deployment for both Pull Request and Push(to master and release branches) to Dev environment. 
  - This environment and Release stage can later be used to run integration tests before promoting deployment to higher stages.  
- Added Continous Monitoring to the App Service via Application Insights with Release Pipeline task
- Modified Release Pipeline to have Dev, Test, Staging & Production to promote DevOps practices. 
  - Have stage specific variables to use same code with various configurations.
  - Management of Terraform state file via appending statefile name with stage name in pipeline.

## Code Management / Github
- Enabled master branch protection in GitHub repository with status checks requirement for safe contribution. 

