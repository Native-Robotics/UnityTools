namespace NativeRobotics.Utils.RobotView
{
    public class KawasakiCp180LMeshRobotView : MeshRobotView
    {
        public override void UpdateView(float[] state)
        {
            state[2] = state[2] - state[1];
            base.UpdateView(state);
        }
    }
}