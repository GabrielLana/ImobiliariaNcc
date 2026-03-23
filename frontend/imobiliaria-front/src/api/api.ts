import axios from "axios"

export const api = axios.create({
  baseURL: "/api"
})

api.interceptors.request.use(config => {

  const token = localStorage.getItem("token")

  if (token)
    config.headers.Authorization = `Bearer ${token}`

  return config
})

api.interceptors.response.use(

  (response) => response,

  (error) => {
    if (!error.response) {
      alert("Erro de conexão com o servidor")
      return Promise.reject(error)
    }

    const status = error.response.status
    const token = localStorage.getItem("token")

    const url = error.config.url
    if (status === 401 && !token && !url.includes("/login")) {

      localStorage.clear()

      window.location.href = "/"
      alert("Sessão invalidada. Redirecionando ao login...")
      return Promise.reject(error)
    }

    const message =
      error.response.data?.errors?.join("\n") ||
      error.response.data?.error ||
      "Erro inesperado"

    alert(message)

    return Promise.reject(error)

  }

)