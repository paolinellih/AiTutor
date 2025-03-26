using Microsoft.AspNetCore.Mvc;
using AiTutor.Application.Services;

namespace AiTutor.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class AiController(AiService aiService) : ControllerBase
    {
        [HttpPost("explain")]
        public async Task<IActionResult> Explain([FromBody] string prompt)
        {
            var aiResponse = await aiService.GetAiResponseAsync(prompt);
            return Ok(new 
            {
                aiResponse.Id,
                aiResponse.Prompt,
                ResponseText = aiResponse.Response,
                GeneratedAt = aiResponse.GeneratedAt
            });
        }
    }
}
