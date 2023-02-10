using static MachineLearning.MLModel;

namespace MachineLearning.Services
{
    public interface IMLService
    {

        Task<string> uploadImage(IFormFile image);
        string deleteImage(string path);
        public ModelOutput predictBird(string imagePath);



    }

    public class MLService : IMLService
    {
       

        public async Task<string> uploadImage(IFormFile image)
        {
       
            var path = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images", image.FileName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            return path;
        } 
        
        public string deleteImage(string imagePath)
        {
            if (string.IsNullOrEmpty(imagePath))
                return ("La ruta de la imagen no fue suministrada");

            try
            {
                System.IO.File.Delete(imagePath);
                return ("La imagen fue eliminada");
            }
            catch (Exception ex)
            {
                return ("Error al eliminar la imagen: " + ex.Message);
            }
        }

        public ModelOutput predictBird(string imagePath)
        {
            //Load sample data
            var sampleData = new MLModel.ModelInput()
            {
                ImageSource = imagePath
            };

            //Load model and predict output
            var result = MLModel.Predict(sampleData);

            return result;
        }
    }
}
