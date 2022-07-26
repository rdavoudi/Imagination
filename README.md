# Image Conversion Service
<b>The goal of this project is to implement a Web API that converts
common web image file formats (e.g., PNG) into JPEG.<br />
This project has been created regarding the below requested scenario:</b>

## Implementation notes

- The current server project is just a template project with OpenTelemetry tracing added on top.
- A `POST` request is made to the `/convert` route.
- The response is either a binary stream with `Content-Type: image/jpeg` or error information.
- The service should be packaged into a [`Dockerfile`](Dockerfile)
  and should be added to  [`docker-compose.yml`](docker-compose.yml)
  with appropriate configuration; publishing the image (e.g. Docker Hub) is not required.
- Using any existing solution as long as the above holds.

## Assumptions

- The service will be orchestrated by Kubernetes, which is configured to auto-scale it based on
  CPU or memory load.
- The service is called once for each search request; search requests must finish in
  under a second total (i.e., including the actual search).
- The service called once on each imported reference image; this is an off-line process and
  may take much longer.
- Searched images are typically already JPEG at about 60 KB of compressed
  file size, but can be any typical web image format.
- Searched images typically have around 1024 x 1024 pixels in total.
- Import images may be much larger.
- The `Content-Type` header is generally not trustworthy.

