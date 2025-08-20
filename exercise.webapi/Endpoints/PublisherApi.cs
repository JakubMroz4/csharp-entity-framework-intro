using exercise.webapi.Factories;
using exercise.webapi.Repository;

namespace exercise.webapi.Endpoints
{
    public static class PublisherApi
    {
        public static void ConfigurePublishersApi(this WebApplication app)
        {
            var publishers = app.MapGroup("publishers");

            publishers.MapGet("/", GetPublishers);
            publishers.MapGet("/{id}", GetPublisher);
        }

        private static async Task<IResult> GetPublishers(IPublisherRepository publisherRepository)
        {
            var publishers = await publisherRepository.GetAllPublishers();

            var dtos = publishers.Select(PublisherFactory.DtoFromPublisher).ToList();
            return TypedResults.Ok(dtos);
        }

        private static async Task<IResult> GetPublisher(IPublisherRepository publisherRepository, int id)
        {
            var publisher = await publisherRepository.GetPublisher(id);

            if (publisher is null)
            {
                return TypedResults.NotFound();
            }

            var dto = PublisherFactory.DtoFromPublisher(publisher);
            return TypedResults.Ok(dto);
        }
    }
}
