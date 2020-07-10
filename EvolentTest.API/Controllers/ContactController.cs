using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EvolentTest.Common;
using EvolentTest.Common.Constants;
using EvolentTest.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EvolentTest.API.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("/api/[controller]")]
    public class ContactController : ControllerBase
    {
        private IContactService _contactService;
        private ILogService _logger;
        public ContactController(IContactService contactService, ILogService logger)
        {
            _contactService = contactService;
            _logger = logger;
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddContact([FromBody] ContactModel contactModel)
        {
            var result = new BaseResponse();

            try
            {
                #region Validate the input

                if (contactModel == null || !ModelState.IsValid)
                {
                    result.Success = false;
                    result.Errors = new List<string>() { MessageConstant.REQUEST_IS_NULL };
                    return StatusCode(StatusCodes.Status400BadRequest, result);
                }

                //Validate Email
                if (!Regex.IsMatch(contactModel.Email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
                {
                    result.Success = false;
                    result.Errors = new List<string>() { MessageConstant.EMAIL_IS_NOT_VALID };
                    return StatusCode(StatusCodes.Status400BadRequest, result);
                }

                //Check Email
                if (await _contactService.IsEmailExist(contactModel.Email))
                {
                    result.Success = false;
                    result.Errors = new List<string>() { MessageConstant.EMAIL_ALREADY_EXIST };
                    return StatusCode(StatusCodes.Status400BadRequest, result);
                }

                //Validate Phone Number
                if (!Regex.IsMatch(contactModel.PhoneNumber, @"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$", RegexOptions.IgnoreCase))
                {
                    result.Success = false;
                    result.Errors = new List<string>() { MessageConstant.PHONE_IS_NOT_VALID };
                    return StatusCode(StatusCodes.Status400BadRequest, result);
                }

                //Check Phone Number
                if (await _contactService.IsPhoneNumberExist(contactModel.PhoneNumber))
                {
                    result.Success = false;
                    result.Errors = new List<string>() { MessageConstant.PHONE_ALREADY_EXIST };
                    return StatusCode(StatusCodes.Status400BadRequest, result);
                }

                #endregion

                var user = await _contactService.AddContact(contactModel);

                result = new BaseResponse()
                {
                    Success = true,
                    Result = user
                };

                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                _logger.Error(ex.Message);

                result.Success = false;
                result.Errors = new List<string>() { ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }
        }

        [HttpGet("all")]
        public IActionResult GetAllConatctList([FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = new BaseResponse();

            try
            {
                var userList = _contactService.GetAllContacts(page, pageSize);

                result = new BaseResponse()
                {
                    Success = true,
                    Result = userList
                };

                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                _logger.Error(ex.Message);

                result.Success = false;
                result.Errors = new List<string>() { ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateContact(string id, [FromBody] ContactModel contactModel)
        {
            var result = new BaseResponse();

            try
            {
                #region Validate the input

                if (contactModel == null || !ModelState.IsValid || string.IsNullOrEmpty(id))
                {
                    result.Success = false;
                    result.Errors = new List<string>() { MessageConstant.REQUEST_IS_NULL };
                    return StatusCode(StatusCodes.Status400BadRequest, result);
                }

                //Validate Email
                if (!Regex.IsMatch(contactModel.Email, @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase))
                {
                    result.Success = false;
                    result.Errors = new List<string>() { MessageConstant.EMAIL_IS_NOT_VALID };
                    return StatusCode(StatusCodes.Status400BadRequest, result);
                }

                //Check Email
                if (await _contactService.IsEmailExist(contactModel.Email))
                {
                    result.Success = false;
                    result.Errors = new List<string>() { MessageConstant.EMAIL_ALREADY_EXIST };
                    return StatusCode(StatusCodes.Status400BadRequest, result);
                }

                //Validate Phone Number
                if (!Regex.IsMatch(contactModel.PhoneNumber, @"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$", RegexOptions.IgnoreCase))
                {
                    result.Success = false;
                    result.Errors = new List<string>() { MessageConstant.PHONE_IS_NOT_VALID };
                    return StatusCode(StatusCodes.Status400BadRequest, result);
                }

                //Check Phone Number
                if (await _contactService.IsPhoneNumberExist(contactModel.PhoneNumber))
                {
                    result.Success = false;
                    result.Errors = new List<string>() { MessageConstant.PHONE_ALREADY_EXIST };
                    return StatusCode(StatusCodes.Status400BadRequest, result);
                }

                #endregion

                contactModel.ContactId = Guid.Parse(id);

                var user = await _contactService.UpdateContact(contactModel);

                result = new BaseResponse()
                {
                    Success = true,
                    Result = user
                };

                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                _logger.Error(ex.Message);

                result.Success = false;
                result.Errors = new List<string>() { ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteContact(string id)
        {
            var result = new BaseResponse();

            try
            {
                #region Validate the input

                if (string.IsNullOrEmpty(id))
                {
                    result.Success = false;
                    result.Errors = new List<string>() { MessageConstant.REQUEST_IS_NULL };
                    return StatusCode(StatusCodes.Status400BadRequest, result);
                }

                #endregion

                var user = await _contactService.DeleteContact(Guid.Parse(id));

                result = new BaseResponse()
                {
                    Success = true,
                    Result = user
                };

                return new OkObjectResult(result);
            }
            catch (Exception ex)
            {
                // return error message if there was an exception
                _logger.Error(ex.Message);

                result.Success = false;
                result.Errors = new List<string>() { ex.Message };
                return StatusCode(StatusCodes.Status500InternalServerError, result);
            }
        }
    }
}
