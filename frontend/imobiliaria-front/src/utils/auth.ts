export function getUserFromToken(){

    const token = localStorage.getItem("token")

    if(!token) return null

    const payload = JSON.parse(atob(token.split('.')[1]))

    return {
        id: payload["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier"],
        nome: payload["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"]
    }

}