using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_SampleApp.Models
{
    interface IMappable<E, DTO> where E: IEntity
    {
        DTO MapToDTO(E entity);
        E MapToEntity(E entity);
    }
}
