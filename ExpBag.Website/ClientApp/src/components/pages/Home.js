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
var react_redux_1 = require("react-redux");
var AuthStore = require("../../store/authStore");
// type HomeState = {};
var Home = /** @class */ (function (_super) {
    __extends(Home, _super);
    function Home(props) {
        var _this = _super.call(this, props) || this;
        _this.componentDidMount = function () {
            console.log("Home did mount");
        };
        _this.componentDidUpdate = function () {
        };
        _this.render = function () {
            return (React.createElement(React.Fragment, null,
                React.createElement("h1", null, "ADEPTE XIAAOOOOOOO")));
        };
        return _this;
    }
    return Home;
}(React.PureComponent));
exports.default = react_redux_1.connect(function (state) { return state.auth; }, AuthStore.actionCreators)(Home);
//# sourceMappingURL=Home.js.map