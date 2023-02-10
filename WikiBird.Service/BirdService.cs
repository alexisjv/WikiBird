

using WikiBird.Model.Entities;

namespace WikiBird.Service
{
    public interface IBirdService
    {

        List<Bird> getAll();
        Bird getBird(int id);
        void createBird(Bird bird);
        void deleteBird(int id);
        //String predictBird(String image);
    }



    public class BirdService : IBirdService
    {
        public WikiBirdContext wikiBirdContext;

        public BirdService(WikiBirdContext wikiBirdContext)
        {
            this.wikiBirdContext = wikiBirdContext;
        }

        public void createBird(Bird bird)
        {
            wikiBirdContext.Birds.Add(bird);
            wikiBirdContext.SaveChanges();
        }

        public void deleteBird(int id)
        {
            var bird = wikiBirdContext.Birds.Find(id);
            wikiBirdContext.Remove(bird);
            wikiBirdContext.SaveChanges();
        }

        public List<Bird> getAll()
        {
            var birds = wikiBirdContext.Birds.OrderBy(b => b.Name).ToList();
            return birds;
        }

        public Bird getBird(int id)
        {

            Bird bird = wikiBirdContext.Birds.Find(id);
            if (bird != null)
            {
                return bird;
            }
            else
            {
                throw new Exception("No se encontró un ave con id: " + id);
            }
        }

        /*  public String predictBird(String image)
          {
              //Load sample data
              var sampleData = new BirdMachineLearning.MLModel.ModelInput()
              {
                  ImageSource = @"C:\Users\Alexis\Desktop\descarga.jpg",
              };

              //Load model and predict output
              var result = BirdMachineLearning.MLModel.Predict(sampleData);

              return result.Prediction;

          }*/
    }
}