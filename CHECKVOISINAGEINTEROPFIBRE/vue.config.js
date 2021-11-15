const CopyPlugin = require("copy-webpack-plugin");
module.exports = {
    publicPath: '/CheckVoisinageInterop/',
    devServer: {
        proxy: 'https://localhost:44305/',
    },
    configureWebpack: {
        plugins: [
            new CopyPlugin({
              patterns: [
                { from: "web.config", to: "../dist" }
              ],
            }),
        ],
    },
    css: {
        extract: true
     } 
}