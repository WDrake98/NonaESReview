using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace NonaESCodeChallage.Pages
{
    public partial class SoftwareVersions
    {
        public event Action OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();

        [Inject] private NonaESCodeChallage.Data.SoftwareVesionService SoftwareService { get; set; }

        private string ErrorMessages;

        private string SoftwareVersionToSearch;

        private string SearchLabel = "All Software Version:";

        private IEnumerable<SoftwareEnhanced> EnhancedSoftware;
    
        protected override async Task OnInitializedAsync()
        {
            EnhancedSoftware = await SoftwareService.GetSoftwareVersionsAsync();
            SetSearchLabel();
        }

        private void SetSearchLabel() {
             SearchLabel = $"All Software Versions ({EnhancedSoftware?.Count()}):";            
        }

        private async void SearchSoftware()
        {

            if (!string.IsNullOrWhiteSpace(SoftwareVersionToSearch))
            {
                if (int.TryParse(SoftwareVersionToSearch, out int parsedSearchResultIsInt))
                {
                    SoftwareVersionToSearch += ".0";
                }

                if (!Version.TryParse(SoftwareVersionToSearch, out Version parsedSearchResult))
                {
                    ErrorMessages = $"Sorry, {SoftwareVersionToSearch} is not a valid version value";
                    return;
                }
                else
                {
                    EnhancedSoftware = await SoftwareService.SearchSoftwareVersionsAsync(parsedSearchResult);                   
                }

                SearchLabel = $"Showing {EnhancedSoftware.Count()} Search Results for {SoftwareVersionToSearch}:";
                ErrorMessages = "";
            }
            else
            {
                ErrorMessages = "Please Enter a valid search value in for format [major].[minor].[patch] or clean Show All to see all versions";
                EnhancedSoftware = null;
                SearchLabel = "";
            }
        }

        private async void ResetList()
        {
            EnhancedSoftware = await SoftwareService.GetSoftwareVersionsAsync();           
            ErrorMessages = "";
            SoftwareVersionToSearch = "";
            SetSearchLabel();
        }
    }
}