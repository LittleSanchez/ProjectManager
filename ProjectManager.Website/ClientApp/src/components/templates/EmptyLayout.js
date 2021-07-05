"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (Object.prototype.hasOwnProperty.call(b, p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
Object.defineProperty(exports, "__esModule", { value: true });
var React = require("react");
var reactstrap_1 = require("reactstrap");
var EmptyLayout = /** @class */ (function (_super) {
    __extends(EmptyLayout, _super);
    function EmptyLayout() {
        return _super !== null && _super.apply(this, arguments) || this;
    }
    EmptyLayout.prototype.render = function () {
        return (React.createElement(React.Fragment, null,
            React.createElement(reactstrap_1.Container, null, this.props.children)));
    };
    return EmptyLayout;
}(React.PureComponent));
exports.default = EmptyLayout;
//# sourceMappingURL=EmptyLayout.js.map