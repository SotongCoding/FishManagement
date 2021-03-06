using System.Linq;

public class Enums {

    public static string[] trivias =

        {
            //1. Pemberian Makan
            "Pemberian pakan pada Ikan lele sebaiknya di lakukan 3-4 kali perhari.\n" +
            "Jika menggunakan pelet, jumlah pelet yang di tebar sebaiknya sekitar 3% dari Berat Masa ikan lele dalam Kolam.\n" +
            "Untuk penghitungan Berat dapat dilakukan pengimbangan 4-6 Lele secara acak dari dalam kolam untuk mencari berat rata-rata dan di kali jumlah ikan pada kolam",
            //2. Penebaran Bibit pada Kolam
            "Tidak ada batasan dalam berapa banyak bibit yang ditebar pada Kolam Budidaya.\n" +
            "Hanya saja akan lebih baik jika tidak terlalu banyak agar tidak mengganggu pertumbuhan ikan yang lain di karenakan Ikan Lele yang rakus dan terkadang bersifat kanibal\n" +
            "Akan baik jika bibit yang disebar sekitar 100-150 per Kolam dengan ukuran 2x3x1 m. Bisa juga mengurangi jumlah ikan jika dirasa ukuran ikan sudah terlalu besar.",
            // 3. Protein yang diberikan pada Ikan
            "Sebaiknya pemberian pakan pelet berdasarkan kadar protein pada lele berbanding terbalik dengan ukuran lele.\n" +
            "Pakan dengan kandungan protein tinggi diberikan pada saat masih kecil dan kandungan protein yang lebih rendah diberikan pada ikan yang lebih besar.\n" +
            "Hal ini menjadi pertimbangan karena harga pelet yang berbeda serta kebutuhan ikan yang semakin banyak.\n" +
            "Tapi tenang, kebutuhan protein bisa di penuhi dengan memberikan cacing, ikan Runcah, ayam, atau yang lain.",
        };
    public static string[] tutorDialog = {
        //Tutor 0 - Salam perkenalan
        "Ahh, Selamat Datang.\n Kau pasti orang yang akan melanjutkan tempat budidaya milik paman Aldi.\n" +
        "Namaku Tiara, salam kenal. Disini aku akan membantu mu untuk menjelaskan apa saja yang ada di sini",
        //Tutor 1 - Menggerakkan Karakter 
        "Disini aku akan membantumu mengurus Kolam. Kau bisa meng-'Klik' Kolam untuk melihat bagaimana kondisi kolam tersebut.",
        //Tutor 2 - Melakukan penjualan Ikan
        "Di sebelah kiri akan muncul orang dalam beberapa waktu sampai 3 orang. 'klik' orang tersebut untuk melihat pesanan.\nSehingga kau bisa menentukan ikan apa yang harus kau kembangbiakkan.",
        //Tutor 3 - Menjual Ikan yang sisa atau tidak diperlukan
        "Ah, iya. Orang-orang tersebut hanya akan membeli ikan sesuai dengan pesanan dan kau tidak bisa memanen ikan di kolam sampai keranjangmu benar-benar kosong.\n" +
        "Untuk itu pergi ke orang dibawah untuk menjual ikan sisa, meski harganya murah sih.",
        //Tutor 4 - Melihat Trivia
        "Dan juga saat keranjang mu masih kosong jika kau ke orang itu, Dia akan memberimu Informasi yang mungkin berguna.\nDan satu lagi terkadang dia suka mengatakan hal yang sama berulang jadi harap sabar.",
        //Tutor 5 - Melihat kolam pertama kali
        "Tekan 'Persiapan' untuk membuat Kolam siap untuk digunakan.",
        //Tutor 6 - Melihat toko untuk pertama kali
        "Selamat datang di toko Ayahku.\nKau bisa membeli kebutuhan dari bibit Ikan, Pakan, dan lainnya untuk melakukan budidaya disini.\nSilakan melihat lihat.",
        // Tutor 7 - Melihat tugas untuk pertama kali
        "Sebelah kiri tersebut merupakan daftar tugas yang harus kau lakukan.\n Selain itu kau bisa melihat daftar pesanan dari Orang-orang yang sudah kau ambil disini.",
        //Tutor 8 Batasan variable pada Kolam
        "Pastikan bahwa kondisi kolam tetap pada nilai tertentu.",
        //Tutor 9 Kapan Ikan bisa di Panen
        "Juga ikan bisa di panen jika berat nya sudah mencapai 50g. Jangan lupa pastikan mereka tidak dalam keadaan lapar.",
        //tutor 10 Indikator untuk Ikan
        "Perhatikan juga kondisi pada Ikan juga.",
        //Tutor 11 Aksi pada kolam
        "Kau bisa mengatur kondisi Kolam dan Ikan melalui tombol yang disediakan.",
        //Tutor 12 Mergarahkan ke toko
        "Sekarang buka toko untuk membeli bibit Ikan dan keperluan yang lain.",
        //Tutor 13 Oksigen
        "Tekan mesin ini jika Oksigen pada kolam sudah di bawah 3mg/L.\nOh iya pastikan Uangmu cukup saat menyalakannya.\nKarena mesin ini menggunakan koin sebagai bahan bakar.",
        //Tutor 14 Basket Panen
        "Kalian bisa menentukan berapa banyak ikan di melalui tombol angka yang ada. Kemudian akan muncul  Indikator \"Basket\" di atas kepala",
        //Tutor 15 Penyakit
        "Jika Ikan terkena penyakit segera obati, beli obat yang sesuai dengan penyakit yang di derita Ikan",
        //Tutor 16 Disarankan membeli pakan
        "Aku sarankan membeli pakan terlebih dahulu sebelum membeli bibit.",
        //Tutor 17
        "Jangan lupa juga membeli beberapa batu kapur dan daun pepaya untuk mengontrol pH"
    };
    public static string ConverterNumber (float number) {
        string reslut = "0";

        if (number >= 1000 && number <= 999999) {
            reslut = (number / 1000).ToString ("0.00") + "K";
        } else if (number >= 1000000 && number <= 999999999) {
            reslut = (number / 1000000).ToString ("0.00") + "M";
        } else if (number >= 1000000000) {
            reslut = (number / 1000000000).ToString ("0.00") + "B";
        } else {
            reslut = number.ToString ();
        }

        return reslut;
    }
    public static string GetTrivia () {

        return "Taukah Kamu ? \n" + trivias[UnityEngine.Random.Range (0, trivias.Length)];
    }

    public static string ShowTutor (int kodeTutor, UnityEngine.RectTransform target) {
        if (new int[] { 2, 3, 4, 6, 7, 12, 16, 17 }.Contains (kodeTutor)) {
            target.anchoredPosition = new UnityEngine.Vector2 (462, 0);
            target.localScale = new UnityEngine.Vector3 (1, 1, 1);
        } else {
            target.anchoredPosition = new UnityEngine.Vector2 (-462, 0);
            target.localScale = new UnityEngine.Vector3 (-1, 1, 0);
        }

        return tutorDialog[kodeTutor];
    }

    public static void DebugText (string text) {
        var debug = new GameDebugger ();

        debug.Init ();
        debug.AddText (() => text);
    }
}

public enum pakan_type {
    p30_40 = 1,
    p41_60 = 2,
    p61_80 = 3

}
public enum type_kolam {
    budidaya,
    benih,
    empty
}
public enum type_air {
    empty,
    tawar,
    laut
}
public enum type_penyakit {
    empty = 0,
    penyakit1 = 11,
    penyakit2 = 12,
    penyakit3 = 13
}
public enum type_ikanBudidya {
    empty,
    leleLokal,
    leleJumbo,
    leleSangkuriang,
    nilaHitam,
    nilaGesit,
    nilaNirwana,
    kerapu
}

public enum Item_type {
    benih,
    pakan,
    medic,
    other,
    none

}

public enum type_ikansize {
    kecil = 3,
    sedang = 2,
    besar = 1
}

public enum obat_type {
    kolam,
    ikan
}

public enum type_time {
    feeding,
    update_data_ikan,
    update_data_kolam,
    endTime
}

public enum type_NPCRole {
    none,
    questing,
    selling,
    selling_questing
}

public enum status_questing {
    start,
    progress,
    done,
    finished
}