using Microsoft.AspNetCore.Mvc;
using rs232WebHaberleşme.Data;
using rs232WebHaberleşme.Models;
using System.IO.Ports;

namespace RS232WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SerialPortController : ControllerBase
    {
        private static SerialPort _serialPort = null!;  // Null olamayacağını belirtmek için null! operatörü kullanılıyor.
        private static SerialPort _serialPort2 = null!;  // Null olamayacağını belirtmek için null! operatörü kullanılıyor.

        static SerialPortController()
        {
            _serialPort = new SerialPort("COM1", 9600);
            _serialPort.Open();
            _serialPort2 = new SerialPort("COM3", 9600);
            _serialPort2.Open();
        }

        [HttpPost("gonder")]
        public IActionResult Gonder([FromBody] string mesaj)
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.WriteLine(mesaj);
                return Ok(new { durum = "Mesaj gönderildi" });
            }
            return BadRequest(new { hata = "Seri port açık değil" });
        }

        [HttpGet("al")]
        public IActionResult Al()
        {
            if (_serialPort2.IsOpen)// && _serialPort2.BytesToRead > 0)
            {
                string gelenVeri = _serialPort2.ReadLine();
                return Ok(new { gelen_veri = gelenVeri });
            }
            return Ok(new { gelen_veri = string.Empty });
        }

        ~SerialPortController()
        {
            if (_serialPort != null && _serialPort.IsOpen)
                _serialPort.Close();
            if (_serialPort2 != null && _serialPort2.IsOpen)
                _serialPort2.Close();
        }
    }
}
