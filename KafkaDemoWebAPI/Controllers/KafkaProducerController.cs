using System.Threading.Tasks;
using KafkaDemoWebAPI.Constants;
using KafkaDemoWebAPI.Interfaces;
using KafkaDemoWebAPI.Messages.DespatchAdvice;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace KafkaDemoWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class KafkaProducerController : ControllerBase
    {
        private readonly IKafkaProducer<string, DespatchAdvice> _kafkaProducer;
        public KafkaProducerController(IKafkaProducer<string, DespatchAdvice> kafkaProducer)
        {
            _kafkaProducer = kafkaProducer;
        }
		 
        [HttpPost]
        [Route("SendDispatchAdviceMessage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerOperation("SendDispatchAdviceMessage", "This endpoint can be used to send DispatchAdviceMessage, but for demo produces dummy message in Kafka Topic")]
        public async Task<IActionResult> ProduceMessage(DespatchAdvice request)
        {
            await _kafkaProducer.ProduceAsync(KafkaTopics.SampleTopic, null, request);

            return Ok("Sending DespatchAdvice message In Progress");
        }
    }
}