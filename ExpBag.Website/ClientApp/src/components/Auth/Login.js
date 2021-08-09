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
//& RouteComponentProps<{ startDateIndex: string }>; // .
var Login = /** @class */ (function (_super) {
    __extends(Login, _super);
    function Login(props) {
        var _this = _super.call(this, props) || this;
        _this.componentDidMount = function () {
            console.log("Login mounted");
        };
        _this.componentDidUpdate = function () {
            console.log("Login updated");
        };
        _this.onFormSubmit = function (e) {
            e.preventDefault();
            console.log('1');
            _this.props.requestLogin({
                email: e.currentTarget['email'].value,
                password: e.currentTarget['password'].value
            });
            _this.props.history.push('/');
            console.log('2');
        };
        _this.render = function () {
            return (React.createElement(React.Fragment, null,
                React.createElement("div", { className: "row justify-content-center" },
                    React.createElement("div", { className: "col-4" },
                        React.createElement("form", { method: "POST", action: "#", onSubmit: _this.onFormSubmit },
                            React.createElement("div", { className: "form-group" },
                                React.createElement("input", { className: "form-control", type: "text", name: "email", id: "email", placeholder: "Email" })),
                            React.createElement("div", { className: "form-group" },
                                React.createElement("input", { className: "form-control", type: "password", name: "password", id: "password", placeholder: "Password" })),
                            React.createElement("div", { className: "form-group" },
                                React.createElement("input", { className: "form-control", type: "submit", value: "Log in" })))))));
        };
        return _this;
    }
    return Login;
}(React.PureComponent));
exports.default = react_redux_1.connect(function (state) { return ({
    auth: state.auth,
    router: state.router
}); }, AuthStore.actionCreators)(Login);
//# sourceMappingURL=Login.js.map