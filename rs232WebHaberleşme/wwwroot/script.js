function mesajGonder() {
    const mesaj = document.getElementById('mesaj').value;
    const timestamp = new Date().toLocaleString();

    fetch('/api/serialport/gonder', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(mesaj)
    })
        .then(response => response.json())
        .then(data => {
            console.log(data.durum);
            ekleMesaj(mesaj, 'sent', timestamp);
        })
        .catch(error => console.error('Hata:', error));
}

function gelenVeriAl() {
    fetch('/api/serialport/al')
        .then(response => response.json())
        .then(data => {
            if (data.gelen_veri) {
                const timestamp = new Date().toLocaleString();
                ekleMesaj(data.gelen_veri, 'received', timestamp);
            }
        })
        .catch(error => console.error('Hata:', error));
}

function ekleMesaj(mesaj, tip, timestamp) {
    const messagesDiv = document.getElementById('messages');
    const messageDiv = document.createElement('div');
    messageDiv.className = `message ${tip}`;
    messageDiv.innerHTML = `${mesaj}<div class="timestamp">${timestamp}</div>`;
    messagesDiv.appendChild(messageDiv);
}

setInterval(gelenVeriAl, 1000);
