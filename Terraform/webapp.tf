 terraform {
  required_version = ">= 0.11" 
 backend "azurerm" {
  storage_account_name = "__terraformstorageaccount__"
    container_name       = "terraform"
    key                  = "terraform.tfstate"
	access_key  ="__storagekey__"
	}
	}
resource "azurerm_resource_group" "dev" {
  name     = "PULTerraform"
  location = "West Europe"
}

resource "azurerm_app_service_plan" "dev" {
  name                = "__appserviceplan__"
  location            = "${azurerm_resource_group.dev.location}"
  resource_group_name = "${azurerm_resource_group.dev.name}"

  sku {
    tier = "Free"
    size = "F1"
  }
}

resource "azurerm_app_service" "dev" {
  name                = "__appservicename__"
  location            = "${azurerm_resource_group.dev.location}"
  resource_group_name = "${azurerm_resource_group.dev.name}"
  app_service_plan_id = "${azurerm_app_service_plan.dev.id}"

}