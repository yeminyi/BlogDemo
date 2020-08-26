const PROXY_CONFIG = [
    {
        context: [
            "/api",
            "/uploads"
        ],
        target: "https://blogdemoapi.azurewebsites.net",
        secure: false
    },
    {
        context: [
            "/avatars"
        ],
        target: "https://localhost:5001",
        secure: false
    }
];

module.exports = PROXY_CONFIG;