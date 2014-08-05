using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Classifieds.Models.SearchViewModels;
using Classifieds.Models.ViewModels;
using System.Collections;
using System.Web.Mvc;

namespace Classifieds.Models.SearchViewModels
{
    public interface ISearchViewModel
    {
        List<SelectListItem> cat { get; set; }
        ListingSearchViewModel listingSearchViewModel { get; set; }
    }
}
