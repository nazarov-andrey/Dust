
namespace Dust.Models
{
	public class Exit : PositionHolder
	{
		public Exit (Position position) : base (position)
		{
		}

		public override int ViewWeight {
			get {
				return int.MinValue;
			}
		}

		public override bool IsActive {
			get {
				return true;
			}
		}
	}
}
