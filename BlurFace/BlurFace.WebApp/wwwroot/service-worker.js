// In development, always fetch from the network and do not enable offline support.
// This is because caching would make development more difficult (changes would not
// be reflected on the first load after each change).
self.addEventListener('fetch', event => {
	const method = event.request.method;
    const url = event.request.url;
    const destination = event.request.destination;
    const referer = event.request.referer;

    console.debug('fetch', url, method, destination, referer);

    if (url.indexOf("/Home") < 0) {
        console.debug('return url', url,);
        return;
    }

    const proxyUrl = `/api/proxy?url=${url}`;
    console.debug('proxyUrl', proxyUrl);

    event.respondWith(async function () {
        return fetch(proxyUrl);
    }());
});
