using Entity;

namespace Views.Impl
{
    public class CameraView : EntityView
    {
        private GameEntity _entity;

        public override void LinkEntity(GameEntity entity)
        {
            base.LinkEntity(entity);

            _entity = entity;
        }

        private void Update()
        {
            
            if (_entity == null)
                return;

            _entity.Position.Value = transform.position;
        }
    }
}