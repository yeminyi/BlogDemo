const PROXY_CONFIG = [
    {
        context: [
            "/api"
        ],
        target: "http://localhost:6000",
        secure: false
    },
    {
        context: [
            "/uploads"
        ],
        target: "https://localhost:6001",
        secure: false
    }
];

module.exports = PROXY_CONFIG;