"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
exports.__esModule = true;
var response_model_1 = require("../../shared-models/response.model");
var ProductViewModelResponse = /** @class */ (function (_super) {
    __extends(ProductViewModelResponse, _super);
    function ProductViewModelResponse() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    return ProductViewModelResponse;
}(response_model_1.ResponseModel));
exports.ProductViewModelResponse = ProductViewModelResponse;
