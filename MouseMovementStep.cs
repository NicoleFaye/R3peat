namespace R3peat
{
    class MouseMovementStep
    {
        private ushort DestinationAbsoluteX;
        private ushort DestinationAbsoluteY;
        private int PauseMillisecondDuration;
        private int Variance;

        public MouseMovementStep(ushort AbsoluteX, ushort AbsoluteY,int PauseMillisecondDuration=500,int Variance=0) {
            this.DestinationAbsoluteX = AbsoluteX;
            this.DestinationAbsoluteY = AbsoluteY;
            this.PauseMillisecondDuration = PauseMillisecondDuration;
            this.Variance = Variance;
        }
        public ushort GetDestinationAbsoluteX() {
            return this.DestinationAbsoluteX;
        }
        public ushort GetDestinationAbsoluteY() {
            return this.DestinationAbsoluteY;
        }
        public int GetPauseMillisecondDuration() {
            return this.PauseMillisecondDuration;
        }
        public int GetVariance() {
            return this.Variance;
        }
        public void SetDesinationX(ushort newX) {
             this.DestinationAbsoluteX=newX;
        }
        public void SetDesinationY(ushort newY) {
             this.DestinationAbsoluteY=newY;
        }
        public void SetPauseDuration(int newDuration) {
             this.PauseMillisecondDuration=newDuration;
        }
        public void SetVariance(int newVariance) {
             this.Variance=newVariance;
        }
    }
}
