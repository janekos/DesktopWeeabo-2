using DesktopWeeabo2.Core.Enums;

namespace DesktopWeeabo2.Core.Interfaces.Misc {

	public interface IDefineCanvasRoutines<out T> {

		T GetRoutineData(CanvasRoutine routine);
	}
}