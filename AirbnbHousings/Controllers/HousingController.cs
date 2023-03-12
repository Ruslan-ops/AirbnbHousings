using Airbnb.Application.Requests.HousingPhotos.Commands.CreateHousingPhoto;
using Airbnb.Application.Requests.HousingPhotos.Commands.DeleteHousingPhoto;
using AirbnbHousings.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Minio;
using Newtonsoft.Json;

namespace AirbnbHousings.Controllers
{
    [ApiController]
    [Route("housings")]
    public class HousingController : BaseController
    {
        private readonly IMapper _mapper;


        public HousingController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet("all")]
        public IActionResult Get()
        {
            return Ok("successsss");
        }

        [HttpGet("list")]
        public List<HousingShort> GetList()
        {
            return null;
        }

        //[HttpPost("uploadN")]
        //public async Task<IActionResult> UploadImage(IFormCollection collection) 
        //{
        //    await Console.Out.WriteLineAsync(collection["housingId"]);
        //    return Ok();
        //}

        [HttpPost("photo/new")]
        public async Task<IActionResult> UploadImage([FromForm] CreateHousingPhotoDto model, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<CreateHousingPhotoCommand>(model);
            command.UserId = 1786;// this.UserId;
            await Mediator.Send(command, cancellationToken);
            return Ok();
        }

        [HttpDelete("photo/delete")]
        public async Task<IActionResult> DeleteImage([FromBody]DeleteHousingPhotoDto model, CancellationToken cancellationToken)
        {
            var command = _mapper.Map<DeleteHousingPhotoCommand>(model);
            command.UserId = 1786;// this.UserId;
            await Mediator.Send(command, cancellationToken);
            return Ok();
        }

        //    [HttpPost("new")]
        //    public async Task<IActionResult> AddHousingAsync([FromBody]HousingCreateDto model)
        //    {
        //        await Console.Out.WriteLineAsync(JsonConvert.SerializeObject(model));
        //        var h = await _postgresService.AddHousingAsync(model);
        //        await Console.Out.WriteLineAsync(JsonConvert.SerializeObject(h));
        //        return Ok();
        //    }

        //    private IActionResult ValidateInt(string numberStr, out int number)
        //    {
        //        number = 0;
        //        try
        //        {
        //            number =  Convert.ToInt32(numberStr);
        //        }
        //        catch(Exception ex)
        //        {
        //            return BadRequest(ex.Message);
        //        }
        //        return null;
        //    }
    }

    public class HousingCreateDto
    {
        public string Description { get; set; }
        public string Title { get; set; }
        public int NightPrice { get; set; }
        public string Address { get; set; }
    }

}
