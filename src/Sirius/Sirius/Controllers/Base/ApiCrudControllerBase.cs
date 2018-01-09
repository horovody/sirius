using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sirius.Filters.Validation;
using Sirius.Logic;
using Sirius.Shared;
using Sirius.Shared.Entities;

namespace Sirius.Controllers.Base
{
    [Route("api/[controller]")]
    [ValidateModel]
    public abstract class ApiCrudControllerBase<TEntity, TModel> : Controller
        where TEntity : class, IEntity
        where TModel : class, IEntityTransferObject
    {
        protected ApiCrudControllerBase(ICrudDataService<TEntity, TModel> service, IUnitOfWork unitOfWork)
        {
            this.Service = service;
            this.UnitOfWork = unitOfWork;
        }

        protected ICrudDataService<TEntity, TModel> Service { get; }
        protected IUnitOfWork UnitOfWork { get; }

        [HttpGet]
        public virtual async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var list = await Service.GetQuery().ToListAsync(cancellationToken);
            return Ok(list);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            var item = await this.Service.GetAsync(id, cancellationToken);
            if (item == null)
                return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Post(TModel model, CancellationToken cancellationToken)
        {
            if (model == null)
                return BadRequest("Model is not set");

            model = await this.Service.CreateAsync(model, cancellationToken);
            await UnitOfWork.SaveChangesAsync(cancellationToken);
            return Ok(model);
        }

        [HttpPut]
        public virtual async Task<IActionResult> Put(TModel model, CancellationToken cancellationToken)
        {
            if (model == null)
                return BadRequest("Model is not set");

            model = await this.Service.UpdateAsync(model, cancellationToken);
            await UnitOfWork.SaveChangesAsync(cancellationToken);
            return Ok(model);
        }

        [HttpDelete]
        public virtual async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var entry = await this.Service.GetAsync(id, cancellationToken);
            if (entry == null)
            {
                return NotFound($"The entry with id {id} is not found or deleted");
            }

            await this.Service.DeleteAsync(entry, cancellationToken);
            await UnitOfWork.SaveChangesAsync(cancellationToken);
            return Ok();
        }
    }
}
