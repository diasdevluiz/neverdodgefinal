using System;
using System.Collections.Generic;
using System.Globalization;

namespace Leauge_Auto_Accept
{
    internal enum Language
    {
        EnUs,
        PtBr
    }

    internal static class Localization
    {
        private static readonly Dictionary<Language, Dictionary<string, string>> Translations = new()
        {
            [Language.PtBr] = new Dictionary<string, string>(StringComparer.Ordinal)
            {
                ["Initializing..."] = "Inicializando...",
                ["League client cannot be found."] = "Cliente do League não encontrado.",
                ["Console width is too small. Please resize it."] = "A largura do console é muito pequena. Ajuste o tamanho.",
                ["Minimum width: {0} | Current width: {1}"] = "Largura mínima: {0} | Largura atual: {1}",
                ["Console height is too small. Please resize it."] = "A altura do console é muito pequena. Ajuste o tamanho.",
                ["Minimum height: {0} | Current height: {1}"] = "Altura mínima: {0} | Altura atual: {1}",
                ["Select primary champion"] = "Selecionar campeão principal",
                ["Primary rune page"] = "Página de runas principal",
                ["Select backup champion"] = "Selecionar campeão reserva",
                ["Backup rune page"] = "Página de runas reserva",
                ["Select a ban"] = "Selecionar um banimento",
                ["Select a backup ban"] = "Selecionar banimento reserva",
                ["Select summoner spell 1"] = "Selecionar feitiço 1",
                ["Select summoner spell 2"] = "Selecionar feitiço 2",
                ["Instant chat messages"] = "Mensagens instantâneas",
                ["Enable auto accept"] = "Ativar aceite automático",
                ["Enable bravery"] = "Ativar Bravery",
                ["Settings"] = "Configurações",
                ["Unselected"] = "Não selecionado",
                ["None"] = "Nenhum",
                ["Enabled"] = "Ativado",
                ["Disabled"] = "Desativado",
                ["Yes"] = "Sim",
                ["No"] = "Não",
                ["Enabled, {0}"] = "Ativado, {0}",
                ["Preload data"] = "Pré-carregar dados",
                ["Instalock pick"] = "Instalock de escolha",
                ["Instalock ban"] = "Instalock de banimento",
                ["Automatically trade pick order"] = "Trocar ordem de escolha automaticamente",
                ["Instantly hover pick"] = "Passar o mouse instantaneamente",
                ["Automatically restart queue"] = "Reiniciar fila automaticamente",
                ["Cancel queue after dodge"] = "Cancelar fila após dodge",
                ["Ban ally hovered champions"] = "Banir campeões mostrados por aliados",
                ["Language"] = "Idioma",
                ["Delay settings"] = "Configurações de atraso",
                ["Preload all data the app will need on launch."] = "Pré-carrega todos os dados que o aplicativo precisa ao iniciar.",
                ["This includes champions list, summoner spells list and more."] = "Inclui lista de campeões, feitiços de invocador e outros dados.",
                ["Instantly lock in when it's your turn to pick."] = "Trava a escolha instantaneamente quando chegar sua vez de pick.",
                ["This will bypass the lock in delay setting."] = "Ignora a configuração de atraso para travar a escolha.",
                ["Instantly lock in when it's your turn to ban."] = "Trava o banimento instantaneamente quando chegar sua vez.",
                ["Automatically trade pick order when someone requests to."] = "Aceita automaticamente pedidos de troca de ordem de escolha.",
                ["Instantly hover champion as soon as joining champ select."] = "Passa o mouse no campeão assim que entrar na seleção.",
                ["In draft pick, it will hover before you are normally able to."] = "Em modo draft, passa o mouse antes do tempo normal.",
                ["Automatically restart queue every few minutes."] = "Reinicia a fila automaticamente após alguns minutos.",
                ["Default is 5 minutes, can be configured in the delays settings."] = "O padrão é 5 minutos, configurável nas opções de atraso.",
                ["Automatically cancel the queue after someone dodges the lobby."] = "Cancela a fila automaticamente após um dodge no lobby.",
                ["Ban selected champions even when allies hover them."] = "Bane os campeões escolhidos mesmo quando aliados os mostram no hover.",
                ["Disable this to respect ally hovers when banning."] = "Desative para respeitar os hovers dos aliados ao banir.",
                ["Toggle between available interface languages."] = "Alterna entre os idiomas disponíveis da interface.",
                ["Adjust different delays."] = "Ajusta diferentes atrasos.",
                ["Delay after which to hover your pick."] = "Atraso para passar o mouse no seu pick.",
                ["Default is 10 seconds."] = "O padrão é 10 segundos.",
                ["Delay after which to lock in your pick, after you are able to."] = "Atraso para travar sua escolha após estar disponível.",
                ["Default is 1000000 seconds."] = "O padrão é 1000000 segundos.",
                ["Time to lock in before your time runs out."] = "Tempo para travar antes de acabar o tempo.",
                ["Do not set too low (less than 1 second), it will cause you to dodge. Default is 1 second."] = "Não defina muito baixo (menos de 1 segundo), isso causará dodge. O padrão é 1 segundo.",
                ["Delay after which to hover your ban."] = "Atraso para passar o mouse no seu ban.",
                ["Default is 2 seconds."] = "O padrão é 2 segundos.",
                ["Time to lock in before your time runs out"] = "Tempo para travar antes de acabar o tempo",
                ["Default is 1 second."] = "O padrão é 1 segundo.",
                ["How long should the queue be before cancelling and restarting it?"] = "Quanto tempo esperar na fila antes de cancelar e reiniciar?",
                ["Default is 300 seconds."] = "O padrão é 300 segundos.",
                ["Delay after which the chat messages will be sent"] = "Atraso para enviar as mensagens de chat",
                ["Default is 0 seconds."] = "O padrão é 0 segundos.",
                ["Pick hover delay upon phase start"] = "Atraso para passar o mouse no início da fase de escolha",
                ["Pick lock delay upon phase start"] = "Atraso para travar a escolha no início da fase",
                ["Pick lock delay before phase end"] = "Atraso para travar a escolha antes do fim da fase",
                ["Ban hover delay upon phase start"] = "Atraso para passar o mouse no ban no início da fase",
                ["Ban lock delay upon phase start"] = "Atraso para travar o ban no início da fase",
                ["Ban lock delay before phase end"] = "Atraso para travar o ban antes do fim da fase",
                ["Max queue time before restart"] = "Tempo máximo de fila antes de reiniciar",
                ["Chat Messages Delay"] = "Atraso das mensagens de chat",
                ["Are you sure you want to close this app?"] = "Tem certeza de que deseja fechar o aplicativo?",
                [" No"] = " Não",
                ["Yes "] = "Sim ",
                ["New message"] = "Nova mensagem",
                ["Edit"] = "Editar",
                ["Delete"] = "Excluir",
                ["Cancel"] = "Cancelar",
                ["Save"] = "Salvar",
                ["Type your message below"] = "Digite sua mensagem abaixo",
                ["Page {0}/{1}"] = "Página {0}/{1}",
                ["Search: "] = "Pesquisar: ",
                ["<- previous page"] = "<- página anterior",
                ["next page ->"] = "próxima página ->",
                ["No messages configured."] = "Nenhuma mensagem configurada.",
                ["Instant chat messages are disabled."] = "Mensagens instantâneas estão desativadas.",
                ["Instant chat messages are enabled."] = "Mensagens instantâneas estão ativadas.",
                ["Language set to PT-BR"] = "Idioma definido para PT-BR",
                ["Language set to EN-US"] = "Idioma definido para EN-US"
            }
        };

        public static Language CurrentLanguage { get; private set; } = Language.EnUs;

        public static void SetLanguage(Language language)
        {
            CurrentLanguage = language;
        }

        public static string Localize(string text)
        {
            if (CurrentLanguage == Language.EnUs || string.IsNullOrEmpty(text))
            {
                return text;
            }

            if (Translations.TryGetValue(CurrentLanguage, out var map) && map.TryGetValue(text, out var translated))
            {
                return translated;
            }

            return text;
        }

        public static string Format(string text, params object[] args)
        {
            string localized = Localize(text);
            return string.Format(CultureInfo.InvariantCulture, localized, args);
        }

        public static string LocalizeBoolean(bool value) => Localize(value ? "Yes" : "No");

        public static string LocalizeEnabled(bool value)
        {
            return value ? Localize("Enabled") : Localize("Disabled");
        }

        public static string FormatEnabledCount(int count)
        {
            return Format("Enabled, {0}", count);
        }

        public static string GetLanguageLabel(Language language)
        {
            return language switch
            {
                Language.PtBr => "PT-BR",
                _ => "EN-US"
            };
        }
    }
}
