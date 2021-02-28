// In development, always fetch from the network and do not enable offline support.
// This is because caching would make development more difficult (changes would not
// be reflected on the first load after each change).
self.addEventListener('fetch', event => {
	const method = event.request.method;
    const url = event.request.url;
    const destination = event.request.destination;
    //console.log('fetch', method, destination, url);

    if (method.toUpperCase() !== 'GET') return;
    if (destination !== "image") return;

    console.log('fetch image', method, destination, url);

    //const base64 = window.btoa(url);
    const blurUrl = `/api/blur/face?url=${url}`;
    console.debug(blurUrl);
    //return fetch(blurUrl);

    event.respondWith(async function () {
        return fetch(blurUrl);
    }());

    /*// https://developer.mozilla.org/ru/docs/Web/API/FetchEvent
    event.respondWith(async function () {        
        return fetch(event.request);
    }());*/
});
