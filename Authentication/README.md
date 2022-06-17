# Authentication and Authorization
마이크로 소프트 ASP.NET Core에서 사용하는 인증에 대해 알아본다.

## Basic
- AuthenticationHandler와 AuthorizationHandler는 개별적으로 호출된다.
    - Authentication이 실패하더라도 Authorization 핸들링이 진행되며 이때 권한을 부여해서 인증을 통과할 수도 있다.
- Ajax를 이용해서 쿠키를 설정할 때 
	- XMLHttpRequest객체의 withCredentials=true로 설정해야 한다.
	- Access-Control-Allow-Origin가 * 이면 안된다. 특정 Origin을 지정해야 한다.
	- 클라이언트: withCredentials=true, 서버: Access-Control-Allow-Credentials=true로 설정해야 한다.

## IdentityServer
- ApiScope와 ApiResource는 개별로 등록되어야 한다.
	- 클라이언트는 ApiResource를 사용하지 않고 ApiScope를 사용한다.
	- ApiResource는 등록된 ApiScope중에서 자신이 제공하는 Scope를 포함시켜야한다.
	- ApiResource의 이름은 JWT의 Issuer이다.

- 클레임 등록
	- 서버
		- id_token에 클레임을 등록하기 위해선 IdentityResource를 추가한다.
		- access_token에 클레임을 등록하기 위해서는 ApiResource에 UserClaim을 추가한다.
		- Client설정 속성 중 AlwaysIncludeUserClaimsInIdToken 값을 true로 하여 Identity.User의 클레임을 id_token 및 access_token에 추가한다.

	- 클라이언트
		- 서버에서 AlwaysIncludeUserClaimsInIdToken을 false로 하는 경우 
		클라이언트의 OpenIdConnectOptions.GetClaimsFromUserInfoEndpoint 속성을 이용해서 User.Identity에 클레임을 추가할 수 있다.
		- OpenIdConnectOptions.ClaimActions속성을 이용해서 추가될 클레임을 설정할 수 있다.

## IdentityServer <-> Asp.Net Authentication(Cookie, OpenIdConnect) Flow

1. User visits the ASP.NET Core site.
2. App asks the default authentication scheme, "Cookies", to authenticate.
	1. The cookie authentication handler attempts to restore the identity from the (signed) cookie information.
	2. Cookie authentication fails because cookie is missing.

3. App asks the default challenge scheme, "oidc", to perform an authentication challenge.
	1. OpenIdConnect authentication handler redirects to the OpenId Connect authentication provider, this is your Identity Server.
	2. User logs in successfully on the Identity Server.
	3. User is POSTed to /signin-oidc which is the remote sign-in address for the OpenId Connect authentication handler.
	4. OpenId Connect authentication middleware handles the /signin-oidc route and retrieves the user information from the sign-in request that was made by Identity Server.
	5. OpenId Connect authentication scheme creates the authentication ticket and asks the configured sign-in scheme to sign in the user.
4. Cookie authentication scheme handles the sign-in process and creates the user identity. It stores the identity in a cookie, so it can be retrieved on future requests without having to go through the whole authentication challenge pipeline again.
5. User is signed in.