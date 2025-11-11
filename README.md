# UCL-NebulaGraph

Præsentation til [denne opgave](https://ucl.kjc.dk/dfu/dfu-exercises-other-databases.html)

## Præsentation
Start præsentation i VS Code med F1 + "revealjs: Open presentation in browser"

## NebulaGraph

Kør compose filen i `dockerz` mappen.

```sh
cd dockerz/
docker compose up -d
```

eller

```sh
cd dockerz/
podman-compose up -d
```

Det starter fire containere op:
- graphd
- storaged0
- metad0
- [web](http://localhost:7001)

Log på Studio med:

| Graphd IP address | Port | Username | Password |
|-------------------|------|----------|----------|
| graphd            | 9669 | root     | root     |

