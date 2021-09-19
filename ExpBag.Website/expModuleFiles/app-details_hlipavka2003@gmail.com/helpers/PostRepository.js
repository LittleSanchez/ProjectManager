class PostRepository {

    apartmentTypes = [
        "APARTMENT", "FLAT"
    ];
    url = "http://localhost:8086/api/posts/";


    async getWithFilters(page, s) {
        let result = await fetch(this.url + `filter?${page ? "page=" + page : ""}&${s ? "s=" + s : ""}`, {
            method: "GET", // *GET, POST, PUT, DELETE, etc.
            // mode: "no-cors", // no-cors, *cors, same-origin
            // cache: "no-cache", // *default, no-cache, reload, force-cache, only-if-cached
            // credentials: "same-origin", // include, *same-origin, omit
            headers: {
                "Content-Type": "application/json",
                // 'Content-Type': 'application/x-www-form-urlencoded',
            },
            // redirect: "follow", // manual, *follow, error
            // referrerPolicy: "no-referrer", // no-referrer, *client
            // body: JSON.stringify(data), // body data type must match "Content-Type" heade
        });

        let data = await result.json();
        console.log(data);
        data.posts = JSON.parse(data.posts);
        return data;
    }

    async getById(id) {
        let result = await fetch(this.url + id, {
            method: "GET", // *GET, POST, PUT, DELETE, etc.
            // mode: "no-cors", // no-cors, *cors, same-origin
            // cache: "no-cache", // *default, no-cache, reload, force-cache, only-if-cached
            // credentials: "same-origin", // include, *same-origin, omit
            headers: {
                "Content-Type": "application/json",
                // 'Content-Type': 'application/x-www-form-urlencoded',
            },
            // redirect: "follow", // manual, *follow, error
            // referrerPolicy: "no-referrer", // no-referrer, *client
            // body: JSON.stringify(data), // body data type must match "Content-Type" heade
        });

        let data = await result.json();

        return data;
    }

    async get() {
        let result = await fetch(this.url, {
            method: "GET", // *GET, POST, PUT, DELETE, etc.
            // mode: "no-cors", // no-cors, *cors, same-origin
            // cache: "no-cache", // *default, no-cache, reload, force-cache, only-if-cached
            // credentials: "same-origin", // include, *same-origin, omit
            headers: {
                "Content-Type": "application/json",
                // 'Content-Type': 'application/x-www-form-urlencoded',
            },
            // redirect: "follow", // manual, *follow, error
            // referrerPolicy: "no-referrer", // no-referrer, *client
            // body: JSON.stringify(data), // body data type must match "Content-Type" heade
        });

        let data = await result.json();

        return data;
    }

    async add(post) {
        let result = await fetch(this.url, {
            method: "POST", // *GET, POST, PUT, DELETE, etc.
            // mode: "cors", // no-cors, *cors, same-origin
            // cache: "no-cache", // *default, no-cache, reload, force-cache, only-if-cached
            // credentials: "same-origin", // include, *same-origin, omit
            headers: {
                "Content-Type": "application/json",
                // 'Content-Type': 'application/x-www-form-urlencoded',
            },
            // redirect: "follow", // manual, *follow, error
            // referrerPolicy: "no-referrer", // no-referrer, *client
            body: JSON.stringify(post), // body data type must match "Content-Type" heade
        });

        return result.status;
    }

    async update(post) {
        let result = await fetch(this.url + `/${post.id}`, {
            method: "PUT", // *GET, POST, PUT, DELETE, etc.
            // mode: "cors", // no-cors, *cors, same-origin
            // cache: "no-cache", // *default, no-cache, reload, force-cache, only-if-cached
            // credentials: "same-origin", // include, *same-origin, omit
            headers: {
                "Content-Type": "application/json",
                // 'Content-Type': 'application/x-www-form-urlencoded',
            },
            // redirect: "follow", // manual, *follow, error
            // referrerPolicy: "no-referrer", // no-referrer, *client
            body: JSON.stringify(post), // body data type must match "Content-Type" heade
        });
        return result.status;
    }

    async delete(post) {
        let result = await fetch(this.url + `/${post.id}`, {
            method: "DELETE", // *GET, POST, PUT, DELETE, etc.
            // mode: "cors", // no-cors, *cors, same-origin
            // cache: "no-cache", // *default, no-cache, reload, force-cache, only-if-cached
            // credentials: "same-origin", // include, *same-origin, omit
            headers: {
                "Content-Type": "application/json",
                // 'Content-Type': 'application/x-www-form-urlencoded',
            },
            // redirect: "follow", // manual, *follow, error
            // referrerPolicy: "no-referrer", // no-referrer, *client
            // body: JSON.stringify(post), // body data type must match "Content-Type" heade
        });
        return result.status;
    }
};

let postRepository = new PostRepository();





export default postRepository;