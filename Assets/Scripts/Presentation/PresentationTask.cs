namespace Polyjam2023
{
    public class PresentationTask
    {
        private System.Action start;
        private System.Action<float> update;
        private System.Action finish;
        private System.Func<bool> isFinished;
        
        public PresentationTask(System.Action start, System.Action<float> update, 
                                System.Action finish, System.Func<bool> isFinished)
        {
            this.start = start;
            this.update = update;
            this.finish = finish;
            this.isFinished = isFinished;
        }

        public void Start()
        {
            start?.Invoke();
        }

        public void Update(float deltaTime)
        {
            update?.Invoke(deltaTime);
        }

        public void Finish()
        {
            finish?.Invoke();
        }
        
        public bool IsFinished()
        {
            return isFinished.Invoke();
        }
    }
}
