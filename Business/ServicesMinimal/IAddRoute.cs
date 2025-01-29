using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Models.ModelMinimal;
using Microsoft.AspNetCore.Http; 

namespace Business.ServicesMinimal
{
    public interface IAddRoute
    {
        void MapRoutes(WebApplication app);
        
    }
}

