// Camera capture for Blazor WebAssembly - getUserMedia + canvas snapshot

window.PlanogramCamera = {
    _stream: null,
    _videoElementId: null,

    startCamera: function (videoElementId) {
        this._videoElementId = videoElementId;
        return new Promise((resolve, reject) => {
            if (!navigator.mediaDevices || !navigator.mediaDevices.getUserMedia) {
                reject(new Error('Camera not supported in this browser.'));
                return;
            }
            const constraints = { video: true, audio: false };
            if (typeof navigator.mediaDevices.getSupportedConstraints === 'function' &&
                navigator.mediaDevices.getSupportedConstraints().facingMode) {
                constraints.video = { facingMode: 'environment' };
            }
            navigator.mediaDevices.getUserMedia(constraints)
                .then(stream => {
                    this._stream = stream;
                    const video = document.getElementById(videoElementId);
                    if (video) {
                        video.srcObject = stream;
                        video.play();
                        resolve();
                    } else {
                        stream.getTracks().forEach(track => track.stop());
                        this._stream = null;
                        reject(new Error('Video element not found.'));
                    }
                })
                .catch(err => reject(err));
        });
    },

    stopCamera: function () {
        if (this._stream) {
            this._stream.getTracks().forEach(track => track.stop());
            this._stream = null;
        }
        const video = document.getElementById(this._videoElementId);
        if (video) {
            video.srcObject = null;
        }
    },

    capturePhoto: function (videoElementId, dotNetHelper) {
        return new Promise((resolve, reject) => {
            const video = document.getElementById(videoElementId);
            if (!video || !video.srcObject) {
                reject(new Error('Video stream not ready.'));
                return;
            }
            const canvas = document.createElement('canvas');
            canvas.width = video.videoWidth;
            canvas.height = video.videoHeight;
            const ctx = canvas.getContext('2d');
            ctx.drawImage(video, 0, 0);
            const dataUrl = canvas.toDataURL('image/jpeg', 0.85);
            if (dotNetHelper) {
                dotNetHelper.invokeMethodAsync('OnPhotoCaptured', dataUrl).then(() => resolve()).catch(reject);
            } else {
                resolve(dataUrl);
            }
        });
    },

    downloadDataUrl: function (filename, dataUrl) {
        const link = document.createElement('a');
        link.href = dataUrl;
        link.download = filename;
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
    }
};
