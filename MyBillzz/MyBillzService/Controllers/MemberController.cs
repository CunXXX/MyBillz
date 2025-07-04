﻿
using DTOs;
using Microsoft.AspNetCore.Mvc;
using MyBillzService.Services;
using static DTOs.BaseModel;

namespace MyBillzService.Controllers;

[ApiController]
[Route("api/v1/[controller]/[action]")]
public class MemberController : ControllerBase
{
    private readonly IMemberService _memberService;

    public MemberController(IMemberService memberService)
    {
        _memberService = memberService;
    }

    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
    {
        try
        {
            var response = await _memberService.LoginAsync(request);

            return Ok(new ApiResponse<LoginResponseDto?>
            {
                Status = new ApiStatus { Code = 0, Message = "登入成功" },
                Data = response
            });
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<LoginResponseDto?>
            {
                Status = new ApiStatus { Code = 401, Message = ex.Message },
                Data = null
            });
        }
    }

}