const PROXY_CONFIG = [
    {
        context: [
            "/api",
            "/uploads"
        ],
        target: "https://localhost:6001",
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