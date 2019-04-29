using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Common;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        Server.Server server = new Server.Server();
        // GET api/game
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var gameStatus = server.GameStatus();
            switch (gameStatus)
            {
                case GameResult.Win:
                    return Content(Utils.Utils.Win, Utils.Utils.ApplicationJson);                    
                case GameResult.Lose:
                    return Content(Utils.Utils.Lose, Utils.Utils.ApplicationJson);                    
                case GameResult.InProgress:
                    return Content(Utils.Utils.InProgress, Utils.Utils.ApplicationJson);                    
                default:
                    return Content(Utils.Utils.Unexpected, Utils.Utils.ApplicationJson);
            }            
        }

        // POST api/game
        [HttpPost]        
        [ProducesResponseType(201)]
        [ProducesResponseType(500)] //Exception
        [ProducesResponseType(401)] //Authorization required (future use)
        public async Task<IActionResult> Post()
        {
            server.CreateBoard(Common.GameType.PlayerVsAI);
            return CreatedAtAction(nameof(Get), Guid.NewGuid().ToString());
        }

        // POST api/game
        //Will return 200 to a valid move (either proper move and turn),
        //403 for illegal move and 
        //409 if it is not the player's turn. 
        //return StatusCode(403, "Illegal Move");            
        [HttpPut]
        [ProducesResponseType(200, Type = typeof(MoveResult))]
        [ProducesResponseType(403)] //Illegal move
        [ProducesResponseType(409)] //Not the player's turn
        [ProducesResponseType(501)] //Unexpected error                                    
        public async Task<IActionResult> Move(Move move)
        {            
            var moveResult = server.Move(move);

            switch (moveResult.PlayerMoveResult)
            {
                case MoveResponse.Sucess:
                    return Content(JsonConvert.SerializeObject(moveResult), Utils.Utils.ApplicationJson);
                case MoveResponse.GameOver:
                    return Content(JsonConvert.SerializeObject(moveResult), Utils.Utils.ApplicationJson);
                case MoveResponse.IllegalMove:
                    return StatusCode(403, "Illegal Move");
                case MoveResponse.NotYourTurn:
                    return StatusCode(409, "Not your turn");
                default:
                    return StatusCode(501, "Unexpected error");
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
