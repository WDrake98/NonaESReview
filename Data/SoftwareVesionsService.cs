using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections;
using System.Linq;

namespace NonaESCodeChallage.Data
{
    public class SoftwareVesionService
    {               
        public Task<IEnumerable<SoftwareEnhanced>> GetSoftwareVersionsAsync()
        {
            var EnhancedSoftware = new List<SoftwareEnhanced>();

            foreach (var versionNumber in SoftwareManager.GetAllSoftware())
            {
                if (versionNumber.Version.EndsWith("."))
                {
                    versionNumber.Version += "0";
                }
                if (Version.TryParse(versionNumber.Version, out Version ParsedVersion))
                {
                    EnhancedSoftware.Add(new SoftwareEnhanced() { Name = versionNumber.Name, Version = versionNumber.Version, SoftwareVersion = ParsedVersion });
                }
            }
            return Task.FromResult((IEnumerable<SoftwareEnhanced>)EnhancedSoftware);
        }

        public Task<IEnumerable<SoftwareEnhanced>> SearchSoftwareVersionsAsync(Version versionToSearch) {
            return Task.FromResult(GetSoftwareVersionsAsync().Result.Where(v => v.SoftwareVersion.CompareTo(versionToSearch) > 0));
        }         
    }
}