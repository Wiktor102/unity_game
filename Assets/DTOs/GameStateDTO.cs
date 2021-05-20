using System.Collections.Generic;

namespace unity_game.Assets.DTOs {
    public class GameStateDTO {
        public int GemCount { get; set; }
        public int HP { get; set; }
        public PointDTO Position { get; set; }
        public IList<PointDTO> Gems { get; set; }
        public IList<PointDTO> Frogs { get; set; }
    }
}