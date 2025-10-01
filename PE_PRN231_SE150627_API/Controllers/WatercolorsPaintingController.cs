using DataAccess.DTO;
using DataAccess.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Repository;

namespace PE_PRN231_SE150627_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WatercolorsPaintingController : Controller
    {
        private readonly ServiceBase<WatercolorsPainting> _service;
        private readonly ServiceBase<Style> _serviceCategory;

        public WatercolorsPaintingController(ServiceBase<WatercolorsPainting> service, ServiceBase<Style> serviceCategory)
        {
            _service = service;
            _serviceCategory = serviceCategory;
        }

        [Authorize(Roles = "1,2")]
        [HttpGet("search")]
        [EnableQuery]
        public async Task<ActionResult<IList<WatercolorsPaintingDTO>>> SearchWatercolorsPainting(
               string? PaintingAuthor,
               decimal? PublishYear)
        {
            var entities = await _service.FindListAsync<WatercolorsPaintingDTO>(
               expression: _ => (string.IsNullOrEmpty(PaintingAuthor) || _.PaintingAuthor.Contains(PaintingAuthor)) &&
                                (!PublishYear.HasValue || _.PublishYear == PublishYear));
            return Ok(entities);
        }

        [Authorize(Roles = "1")]
        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<IList<WatercolorsPaintingDTO>>> GetWatercolorsPainting()
        {
            var entities = await _service.FindListAsync<WatercolorsPaintingDTO>();
            return Ok(entities);
        }

        [Authorize(Roles = "1")]
        [HttpGet("{id}")]
        [EnableQuery]
        public async Task<ActionResult<WatercolorsPaintingDTO>> GetWatercolorsPainting(string id)
        {
            var entity = await _service.FindByAsync(p => p.PaintingId == id);
            if (entity == null)
            {
                return Problem(detail: $"WatercolorsPainting id [{id}] not found", statusCode: 404);
            }
            return Ok(entity.Adapt<WatercolorsPaintingDTO>());
        }

        [Authorize(Roles = "1")]
        [HttpPut]
        public async Task<IActionResult> PutWatercolorsPainting(WatercolorsPaintingRequest watercolorsPaintingRequest)
        {
            var entity = await _service.FindByAsync(p => p.PaintingId == watercolorsPaintingRequest.PaintingId);
            if (entity == null)
            {
                return Problem(detail: $"WatercolorsPainting ID [{watercolorsPaintingRequest.PaintingId}] not found", statusCode: 404);
            }

            if (!await _serviceCategory.ExistsByAsync(p => p.StyleId == watercolorsPaintingRequest.StyleId))
            {
                return Problem(detail: $"Style ID {watercolorsPaintingRequest.StyleId} not found", statusCode: 404);
            }

            watercolorsPaintingRequest.Adapt(entity);
            await _service.UpdateAsync(entity);
            return Ok($"SilverJewelry ID [ {watercolorsPaintingRequest.PaintingId} ] updated successfully.");
        }


        [Authorize(Roles = "1")]
        [HttpPost]
        public async Task<ActionResult<WatercolorsPaintingDTO>> PostWatercolorsPainting(WatercolorsPaintingRequest watercolorsPaintingRequest)
        {
            if (await _service.ExistsByAsync(p => p.PaintingId == watercolorsPaintingRequest.PaintingId))
            {
                return Problem(detail: $"WatercolorsPainting ID [{watercolorsPaintingRequest.PaintingId}] already exists", statusCode: 400);
            }

            if (!await _serviceCategory.ExistsByAsync(p => p.StyleId == watercolorsPaintingRequest.StyleId))
            {
                return Problem(detail: $"Style ID [{watercolorsPaintingRequest.StyleId}] not found", statusCode: 404);
            }
            var watercolorsPainting = new WatercolorsPainting();

            watercolorsPaintingRequest.Adapt(watercolorsPainting);
            watercolorsPainting.CreatedDate = DateTime.Now;

            await _service.CreateAsync(watercolorsPainting);
            return CreatedAtAction(nameof(GetWatercolorsPainting), new { id = watercolorsPainting.PaintingId }, watercolorsPainting.Adapt<WatercolorsPaintingDTO>());
        }

        [Authorize(Roles = "1")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWatercolorsPainting(string id)
        {
            var watercolorsPainting = await _service.FindByAsync(p => p.PaintingId == id);
            if (watercolorsPainting == null)
            {
                return Problem(detail: $"WatercolorsPainting ID [{id}] not found", statusCode: 404);
            }
            await _service.DeleteAsync(watercolorsPainting);
            return Ok($"WatercolorsPainting ID [{id}] deleted successfully.");
        }
    }
}
