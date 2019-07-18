**Rate-limiting pattern**

Rate limiting involves restricting the number of requests that can be made by a client.
A client is identified with an access token which is used for every request to a resource. 
To prevent abuse of the server, APIs enforce rate-limiting techniques. 
Based on the client, the rate-limiting application can decide whether to allow the request to go through or not.
The client makes an API call to a particular resource, the server checks whether the request for this client is within the limit.
If the request is within the limit, then the request goes through.
Otherwise, the API call is restricted.

Some examples of request-limiting rules:
* X requests per timespan
* A certain timespan passed since the last call

The goal is to design a class(-es) which manage rate limits for every provided API resource.

Keep in mind that you don't need to get the solution to be 100% working by the end of the interview. 
What we do care is how do you use your environment, how do you think and write the code.
Feel free to spend some time and to set up the IDE to your preferred settings (e.g. color scheme, R# hotkeys, etc.), if you are not using your own.
You are welcome to ask any questions regarding the requirements, treat us as product owners/analysts/whoever who knows the business.
You are allowed to use anything you used to use during your daily job routine, e.g. stackoverflow, google, etc. 
Use simple in-memory data structures to store the data, don't rely on the particular database.
